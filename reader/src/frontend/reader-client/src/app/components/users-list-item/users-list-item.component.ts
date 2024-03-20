import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-users-list-item',
  standalone: true,
  imports: [NgIf],
  templateUrl: './users-list-item.component.html',
  styleUrl: './users-list-item.component.css'
})
export class UsersListItemComponent {
  @Input() isVisible = true
  @Input() user:any = {}
}
