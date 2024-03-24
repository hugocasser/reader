import { Component, Input, inject } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { NotesService } from '../../services/notes-service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-note-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './note-item.component.html',
  styleUrl: './note-item.component.css'
})
export class NoteItemComponent {
@Input()text: string = '';
@Input()id: string = '';
private router = inject(Router);

auth = inject(AuthService);
notes = inject(NotesService);
isVisible = true;
delete() {
  try{
    this.notes.deleteNote(this.id)
    this.isVisible = false;
    return;
  }
  catch{
    var result = this.auth.refreshToken()
    if(result){
      this.notes.deleteNote(this.id)
      this.isVisible = false;
      return;
    }
    this.router.navigateByUrl('/login');
  }
}
}
