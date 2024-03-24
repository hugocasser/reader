import { Component, inject } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterLink, Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { BooksService } from '../../services/books-service';

@Component({
  selector: 'app-book-create-panel',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgIf, RouterLink],
  templateUrl: './book-create-panel.component.html',
  styleUrl: './book-create-panel.component.css'
})
export class BookCreatePanelComponent {
  bookForm: FormGroup;
  text: string = '';
  booksService = inject(BooksService);
  authService = inject(AuthService);
  router = inject(Router);
  constructor(private formBuilder: FormBuilder) {
    this.bookForm = this.formBuilder.group({
      name: ['', Validators.email],
      categoryId: ['', Validators.required],
      authorId: ['', Validators.required],
      description: ['', Validators.required]
    });
   }

  get formControls() {
    return this.bookForm.controls;
  }

  submit(){
    try{
      this.booksService.CreateBook(this.bookForm.value.name,
         this.text,
          this.bookForm.value.description,
           this.bookForm.value.authorId,
            this.bookForm.value.categoryId,);
    }
    catch{
        let result = this.authService.refreshToken();
        if (result) {
          this.booksService.CreateBook(this.bookForm.value.name,
            this.text,
             this.bookForm.value.description,
              this.bookForm.value.authorId,
               this.bookForm.value.categoryId,);
               return;
        }
        else {
          this.router.navigateByUrl('/login');
        }
    }
  }


  onDrop(event: DragEvent) {
    event.preventDefault();
    const files = event.dataTransfer?.files;
    if (files) {
      this.uploadFiles(files);
    }
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onDragEnter(event: DragEvent) {
    event.preventDefault();
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    const files = input.files;
    if (files) {
      this.uploadFiles(files);
    }
  }

  selectFile() {
    const fileInput = document.getElementById('fileInput') as HTMLInputElement;
    fileInput.click();
  }

  uploadFiles(files: FileList) {
    files[0].text().then(text => {
      this.text = text;
    })
  }
}
