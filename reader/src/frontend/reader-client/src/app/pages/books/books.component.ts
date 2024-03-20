import { Component, OnInit, Input, Output, EventEmitter, inject } from '@angular/core';
import { MenuComponent } from '../../components/menu/menu.component';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { BookItemComponent } from '../../components/book-item/book-item.component';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../login/login.component';
import { BookInfoComponent } from '../../components/book-info/book-info.component';
import { BooksService } from '../../services/books-service';
import { AuthService } from '../../services/auth-service';


@Component({
    selector: 'app-books',
    standalone: true,
    templateUrl: './books.component.html',
    styleUrl: './books.component.css',
    imports: [MenuComponent, PageHeaderComponent, BookItemComponent, CommonModule, LoginComponent, BookInfoComponent]
})
export class BooksComponent implements OnInit{
  availableSizes: number[] = [12, 24, 36, 48, 60, 72, 84, 96];
@Output() sizeChange = new EventEmitter<number>();  // Available page sizes
@Input() selectedSize: number = this.availableSizes[0];
title= 'Books';
curentPage: number = 1;
pageSize: number = localStorage.getItem('pageSize') ? Number(localStorage.getItem('pageSize')) : 12;
curentBooks: any[] = [];
booksService = inject(BooksService);
authService = inject(AuthService);
ngOnInit(): void {
  this.GetBooks();
}
next(){
  this.curentPage++;
  this.GetBooks();
}

prev(){
  this.curentPage--;
  this.GetBooks();
}

private GetBooks(){
  try {
    let books = this.booksService.GetAllBooks(this.curentPage, this.pageSize);
    this.curentBooks = books;
  }
  catch{
    let result = this.authService.refreshToken();
    if (result === true){
      let books = this.booksService.GetAllBooks(this.curentPage, this.pageSize);
      this.curentBooks = books;
    }
    else{
      this.authService.logout();
    }
  }
}
}
