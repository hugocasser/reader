import { Component, OnInit, inject } from '@angular/core';
import { MenuComponent } from '../../components/menu/menu.component';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { CommonModule } from '@angular/common';
import { BookListItem } from '../../models/book';
import { GroupListItem } from '../../models/groups';
import { UserListItem } from '../../models/user';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth-service';
import { BooksService } from '../../services/books-service';
import { GroupService } from '../../services/groups-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [MenuComponent, PageHeaderComponent, CommonModule, FormsModule],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent implements OnInit {


  router = inject(Router);
  bookService = inject(BooksService);
  userService = inject(AuthService);
  searchText: string = '';
  groupsService = inject(GroupService);
  booksCollection: BookListItem[] = 
  [
    { name: 'book1', info: 'book 1 info', type: 'book', id: '12312312312', toInfo() { }, author: 'ada', elementId: 1 },
    { name: 'book2', info: 'book 2 info', type: 'book', id: '1231243243212312', toInfo() { }, author: 'ada', elementId: 1 },
    { name: 'book3', info: 'book 3 info', type: 'book', id: '126678967312312', toInfo() { }, author: 'ada', elementId: 1 },
    { name: 'book4', info: 'book 4 info', type: 'book', id: '12332423512', toInfo() { }, author: 'ada', elementId: 1 }
  ];
  groupsCollection: GroupListItem[] =
   [
    { name: 'Group23', info: 'group 23 info', type: 'group', id: '6456489797', users: 0, books: 0, toInfo() { }, admin: 'ada5465475456', elementId: 1 },
    { name: 'Group24', info: 'group 24 info', type: 'group', id: '6554459909056456', users: 0, books: 0, toInfo() { }, admin: 'ada5465475456', elementId: 1 },
    { name: 'Group25', info: 'group 25 info', type: 'group', id: '60010231203456456456', users: 0, books: 0, toInfo() { }, admin: 'ada5465475456', elementId: 1 },
    { name: 'Group26', info: 'group 25 info', type: 'group', id: '6454324236456456', users: 0, books: 0, toInfo() { }, admin: 'ada5465475456', elementId: 1 }
  ];
  usersCollection: UserListItem[] = 
  [
    { name: 'UserName', info: 'ada4', type: 'user', id: '564765', toInfo() { }, roles: ['user'], elementId: 1 }
  ];
  itemsCollection: any[] = this.usersCollection;
  count = 1;
  title = 'Admin panel'
  users = true;
  books = false;
  groups = false;
  pageSize = localStorage.getItem("pageSize");
  page = 1;

  ngOnInit(): void {
    this.getUsers();
  }

  delete(item: any) {
    switch (item.type) {
      case 'user': {
        let user = new UserListItem();
        user.id = item.id;
        this.usersCollection = this.usersCollection.filter(item => item.id !== user.id);
        this.itemsCollection = this.itemsCollection.filter(item => item.id !== user.id);
        try {
          this.userService.deleteUser(user.id);
          return;
        }
        catch (error: any) {
          this.userService.refreshToken();
          this.userService.deleteUser(user.id);
          return;
        }
      }
      case 'book': {
        let book = new BookListItem();
        book.id = item.id;
        this.booksCollection = this.booksCollection.filter(item => item.id !== book.id);
        this.itemsCollection = this.itemsCollection.filter(item => item.id !== book.id);
        try {
          this.bookService.DeleteBook(book.id);
          return;
        }
        catch (error: any) {
          this.userService.refreshToken();
          this.bookService.DeleteBook(book.id);
          this.userService.clearErrors();
          return;
        }
      }
      case 'group': {
        let group = new GroupListItem();
        group.id = item.id;
        this.groupsCollection = this.groupsCollection.filter(item => item.id !== group.id);
        this.itemsCollection = this.itemsCollection.filter(item => item.id !== group.id);
        try {
          this.groupsService.deleteGroup(group.id);
          return;
        }
        catch (error: any) {
          this.userService.refreshToken();
          this.groupsService.deleteGroup(group.id);
          return;
        }
      }
    }
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
    //this.getUsers();
    this.users = true;
    this.books = false;
    this.groups = false;
    this.itemsCollection = this.usersCollection;
  }

  switchToBooks() {
    if (this.books === true){
      this.router.navigateByUrl('/books/create');
      return;
    }
    //this.getBooks();
    this.users = false;
    this.books = true;
    this.groups = false;
    this.itemsCollection = this.booksCollection;
  }

  switchToGroups(event: MouseEvent) {
          //this.getGroups();
    this.users = false;
    this.books = false;
    this.groups = true;
    this.itemsCollection = this.groupsCollection;
      console.log('Disabled button tapped');
  }

  private getUsers() {
    try {
      this.setUserList();
      return;
    }
    catch (error: any) {
      var reslt = this.userService.refreshToken();
      console.log(reslt);
      if (reslt === true) {
        this.setUserList();
        return;
      }
      this.router.navigateByUrl('/login');
    }
  }

  private setUserList() {

    this.userService.getUsers(Number(this.pageSize), this.page)
      .forEach(user => {
        let item = new UserListItem()
        item.elementId = 1;
        item.name = user.firstName + ' ' + user.lastName;
        item.id = String(user.userId);
        item.roles = user.roles;
        item.toInfo();
        this.usersCollection.push(item);
        this.itemsCollection = this.usersCollection
      });
  }
  private getGroups() {
    try{
      this.setGroupsList();
      return;
    }
    catch (error: any) {
      var reslt = this.userService.refreshToken();
      if (reslt === true) {
        this.setGroupsList();
        return;
      }
      this.router.navigateByUrl('/login');
    }
  }

  private setGroupsList() {
    this.groupsService.getAllGroups(Number(this.pageSize), this.page).forEach(group => {
      let item = new GroupListItem()
      item.elementId = 1;
      item.name = group.name;
      item.id = String(group.id);
      item.books = group.books;
      item.users = group.users;
      item.admin = group.admin;
      item.toInfo();
      this.groupsCollection.push(item);
      this.itemsCollection = this.groupsCollection;
    })
  }

  private getBooks() {
    try{
      this.setBooksList();
      return;
    }
    catch (error: any) {
      var reslt = this.userService.refreshToken();
      if (reslt === true) {
        this.setBooksList();
        return;
      }
      this.router.navigateByUrl('/login');
    }
  }

  private setBooksList() {
    this.bookService.GetAllBooks(Number(this.pageSize), this.page).forEach(book => {

      let item = new BookListItem()
      item.elementId = 1;
      item.name = book.Name;
      item.id = String(book.Id);
      item.author = book.Author;
      item.toInfo();
      this.booksCollection.push(item);
      this.itemsCollection = this.booksCollection;
  });
}}

