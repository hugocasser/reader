import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { MenuComponent } from '../../components/menu/menu.component';
import { AuthService } from '../../services/auth-service';
import { BooksService } from '../../services/books-service';
import { GroupService } from '../../services/groups-service';
import { SignalRService } from '../../services/signalr-service';
import { NotesService } from '../../services/notes-service';
import { NoteItemComponent } from '../../components/note-item/note-item.component';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-reading-page',
  standalone: true,
  imports: [PageHeaderComponent, MenuComponent, NoteItemComponent, NgFor, FormsModule],
  templateUrl: './reading-page.component.html',
  styleUrl: './reading-page.component.css'
})
export class ReadingPageComponent implements OnInit {
nextPage() {
  this.curentPage++;
}
prevPage() {
  this.curentPage--;
}
  title = 'Reading'
  id = 'id';
  @ViewChild("container") container!: ElementRef;

  bookId: string = '';
  groups = inject(GroupService);
  auth = inject(AuthService);
  books = inject(BooksService);
  notesService = inject(NotesService);
  router = inject(Router);
  sendAuto = localStorage.getItem('sendAuto') || '';
  book: any = [];
  notes: any[] = [];
  groupId = localStorage.getItem('groupId') || '';
  curentPage = 1;
  pageSetting = localStorage.getItem("readingPageSize") || '';
  position = 0;
  text ='';

  constructor(private signalR: SignalRService) {
    signalR.hubConnection.on('ReceiveNote', (data: any) => {
      this.notes.push(data);
    });
    signalR.hubConnection.on('RemoveNote', (data: any) => {
      this.notes = this.notes.filter(x => x.id !== data.id);
    });
  }
  ngOnInit(): void {
    this.bookId = localStorage.getItem('bookId') || '';
    try {
      let book = this.books.GetBookById(this.bookId);
      this.book = book;
    }
    catch {
      let result = this.auth.refreshToken();
      if (result === true) {
        let book = this.books.GetBookById(this.bookId);
        this.book = book;
      }
      else {
        this.auth.logout();
      }
    }


  }

  GetNotes() {
    this.notes = [];
    try {
      let notes = this.notesService.getNotesByGroupIdAndBookId(this.groupId, this.bookId, {
        curentPage: this.curentPage * Number(this.pageSetting) - Number(this.pageSetting) + 1,
        pageSize: Number(this.pageSetting)
      });
      notes.forEach(this.notes.push);
    }
    catch {
      let result = this.auth.refreshToken();
      if (result === true) {
        let notes = this.notesService.getNotesByGroupIdAndBookId(this.groupId, this.bookId, {
          curentPage: this.curentPage * Number(this.pageSetting) - Number(this.pageSetting) + 1,
          pageSize: Number(this.pageSetting)
        });
        notes.forEach(this.notes.push);
      }
      else {
        this.auth.logout();
      }
    }
  }

  getClickedCharacter(event: MouseEvent) {
    const container = this.container.nativeElement as HTMLDivElement;
    const rect = container.getBoundingClientRect();
    let posX = 0;
    let posY = 0;
    let pos = 0;
    let b = false;
    let a = false;
    while ((pos < this.text.length) && (posX < Number(rect.right)) && (posY < Number(rect.bottom))){
        if (posX < Number(event.clientX)){
          posX = posX +1.5;
        }
        else{
          b = true;
        }
        if (posY < Number(event.clientY)){
          posY = posY +2;
        }
        else{
          a = true;
        }

        if (b && a){
          this.position = pos;
          break;
        }
        pos++; 
    }
  }

  createNote() {
    this.signalR.sendNoteAsync({text: this.text, bookId: this.bookId, groupId: this.groupId, position: this.position});
  }
}
