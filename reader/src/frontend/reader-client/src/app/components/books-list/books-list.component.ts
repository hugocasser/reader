import { Component } from '@angular/core';
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
  groupId = sessionStorage.getItem('myGroupId');
  myGroupId = sessionStorage.getItem('myGroupId');
  isVisible = false;
  books: any[] = 
  [
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
    {name: 'book', author: 'author'},
  ];

  constructor(private router: Router){}

  BooksList(){
    this.isVisible = !this.isVisible;
  }

  redirectToReading(){
    if(this.groupId === this.myGroupId){
      this.router.navigateByUrl('/reading-page/:id');
    }
  }
}
