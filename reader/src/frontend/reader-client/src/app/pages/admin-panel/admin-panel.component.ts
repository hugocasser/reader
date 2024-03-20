import { Component } from '@angular/core';
import { MenuComponent } from '../../components/menu/menu.component';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { CommonModule } from '@angular/common';
import { AbstractItem } from '../../models/abstract-list-item';
import { BookListItem } from '../../models/book';
import { GroupListItem } from '../../models/groups';
import { UserListItem } from '../../models/user';
import { FormsModule  } from '@angular/forms';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [MenuComponent, PageHeaderComponent, CommonModule, FormsModule],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent {

  constructor() {
    this.filteredItems = this.items;
  }

  items: string[] = ['Item 1', 'Item 2', 'Item 3', 'Item 4', 'Item 5'];
  filteredItems: string[] = [];
  searchText: string = '';

  itemsCollection2: AbstractItem[] = [{ name: 'ada', info: 'ada1', type: 'group', id: 'dasdasdas,', toInfo() { }, elementId: 1 }];
  booksCollection: BookListItem[] = [{ name: 'ada', info: 'ada2', type: 'book', id: 'dasdasdas,', toInfo() { }, author: 'ada', elementId: 1 }];
  groupsCollection: GroupListItem[] = [{ name: 'ada', info: 'ada3', type: 'group', id: 'dasdasdas,', users: 0, books: 0, toInfo() { }, admin: 'ada5465475456', elementId: 1 }];
  usersCollection: UserListItem[] = [{ name: 'ada', info: 'ada4', type: 'user', id: 'dasdasdas,', toInfo() { }, roles: ['ada'], elementId: 1 }];
  itemsCollection: any[] = this.usersCollection;
  count = 1;
  title = 'Admin panel'
  users = true;
  books = false;
  groups = false;
  pageSize = localStorage.getItem("pageSize");

  onSearchChange(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.filteredItems = this.items.filter(item =>
      item.toLowerCase().includes(value.toLowerCase())
    );
  }
  delete(item: any) {

  }
  copyFromLabel(item: any) {
    let textToCopy = "";
    if (item.type === 'user') {
      textToCopy = item.roles.toString();
    } else if (item.type === 'book') {
      textToCopy = item.author;
    } else if (item.type === 'group') {
      textToCopy = item.admin;
    }

    const tempInput = document.createElement('input');
    document.body.appendChild(tempInput);
    tempInput.value = textToCopy;
    tempInput.select();
    document.execCommand('copy');
    document.body.removeChild(tempInput);
  }
  copyFromP(item: any) {
    const textToCopy = item.id;

    const tempInput = document.createElement('input');
    document.body.appendChild(tempInput);
    tempInput.value = textToCopy;
    tempInput.select();
    document.execCommand('copy');
    document.body.removeChild(tempInput);
  }

  showTooltipId(item: any) {
    item.isVisibleId = true;
  }
  showTooltipInfo(item: any) {
    item.isVisibleInfo = true;
  }
  hideTooltipId(item: any) {
    item.isVisibleId = false;
  }

  hideTooltipInfo(item: any) {
    item.isVisibleInfo = false;
  }

  switchToUsers() {
    this.users = true;
    this.books = false;
    this.groups = false;
    this.itemsCollection = this.usersCollection
  }

  switchToBooks() {
    this.users = false;
    this.books = true;
    this.groups = false;
    this.itemsCollection = this.booksCollection
  }

  switchToGroups() {

    let group = new GroupListItem();
    group.name = 'group1';
    group.books = 3;
    group.users = 5;
    group.id = 'idididididididididididididididid';
    group.admin = 'admin';
    group.toInfo();
    this.count++;
    this.groupsCollection.push(group);
    this.users = false;
    this.books = false;
    this.groups = true;
    this.itemsCollection = this.groupsCollection
  }
}
