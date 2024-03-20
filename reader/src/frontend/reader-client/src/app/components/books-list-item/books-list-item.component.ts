import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-books-list-item',
  standalone: true,
  imports: [NgIf],
  templateUrl: './books-list-item.component.html',
  styleUrl: './books-list-item.component.css'
})
export class BooksListItemComponent {
  @Input() isVisible = false
  @Input() book: any = {};
}
