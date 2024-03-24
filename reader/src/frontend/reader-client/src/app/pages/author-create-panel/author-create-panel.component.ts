import { Component, inject } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterLink, Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { BooksService } from '../../services/books-service';

@Component({
  selector: 'app-author-create-panel',
  standalone: true,
  imports: [ RouterLink, ReactiveFormsModule, CommonModule, NgIf],
  templateUrl: './author-create-panel.component.html',
  styleUrl: './author-create-panel.component.css'
})
export class AuthorCreatePanelComponent {
  authorForm: FormGroup;
  auth = inject(AuthService);
  authors = inject(BooksService);
  router = inject(Router);
  constructor(private formBuilder: FormBuilder) {
    this.authorForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      deathDate: ['', Validators.required],
      biography: ['', Validators.required]
    })
  }

  get formControls() {
    return this.authorForm.controls;
  }

  submit(){
    try{
      this.authors.createAuthor(this.authorForm.value.firstName,
        this.authorForm.value.lastName,
        this.authorForm.value.birthDate,
        this.authorForm.value.deathDate,
        this.authorForm.value.biography);
    }
    catch{
      let result = this.auth.refreshToken();
      if (result) {
        this.authors.createAuthor(this.authorForm.value.firstName,
          this.authorForm.value.lastName,
          this.authorForm.value.birthDate,
          this.authorForm.value.deathDate,
          this.authorForm.value.biography);
          return;
      }
      else {
        this.router.navigateByUrl('/login');
      }
  }
}
}
