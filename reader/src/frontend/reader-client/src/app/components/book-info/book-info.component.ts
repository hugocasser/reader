import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Input } from '@angular/core';
import { BooksService } from '../../services/books-service';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-book-info',
  templateUrl: './book-info.component.html',
  styleUrl: './book-info.component.css',
  standalone: true,
  imports: [CommonModule]
})
export class BookInfoComponent implements OnInit {
  @Input() isVisible: boolean = false;
  description: string = '';
  authorFirstName: string = '';
  authorLastName: string = '';
  name: string ='';
  booksService = inject(BooksService);
  authService = inject(AuthService);

  @Input() bookId: string = '';

  ngOnInit(): void {
    try{
      let book = this.booksService.GetBookById(this.bookId);
      this.description = book.description;
      this.authorFirstName = book.authorFirstName;
      this.authorLastName = book.authorLastName;
      this.name = book.bookName;
    }
    catch{
      let result = this.authService.refreshToken();
      if (result === true){
        let book = this.booksService.GetBookById(this.bookId);
        this.description = book.description;
        this.authorFirstName = book.authorFirstName;
        this.authorLastName = book.authorLastName;
        this.name = book.bookName;
      }
      else{
        this.authService.logout();
      }
    }
  }
}
