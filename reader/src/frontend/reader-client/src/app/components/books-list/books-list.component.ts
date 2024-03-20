import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BooksListItemComponent } from '../books-list-item/books-list-item.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-books-list',
  standalone: true,
  imports: [CommonModule, BooksListItemComponent],
  templateUrl: './books-list.component.html',
  styleUrl: './books-list.component.css'
})
export class BooksListComponent {
  groupId = sessionStorage.getItem('groupId');
  myGroupId = sessionStorage.getItem('myGroupId');
  isVisible = false;
  @Input() books: any[] = [ ];

  constructor(private router: Router){}

  BooksList(){
    this.isVisible = !this.isVisible;
  }

  redirectToReading(id: string){
    localStorage.setItem('book', id);
    if(this.groupId === this.myGroupId){
      this.router.navigateByUrl('/reading-page');
    }
  }
}
