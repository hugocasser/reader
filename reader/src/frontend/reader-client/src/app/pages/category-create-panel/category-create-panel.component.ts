import { Component, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth-service';
import { BooksService } from '../../services/books-service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-category-create-panel',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './category-create-panel.component.html',
  styleUrl: './category-create-panel.component.css'
})
export class CategoryCreatePanelComponent {
  categoryForm: FormGroup;
  authService = inject(AuthService);
  categories = inject(BooksService);
  router = inject(Router);
  constructor(private formBuilder: FormBuilder) {
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required]
    })
  }

  get formControls() {
    return this.categoryForm.controls;
  }

  submit(){
    if (this.categoryForm.invalid) {
      return;
    }
    try{
      this.categories.createCategory(this.categoryForm.value.name);
    }
    catch{
      let result = this.authService.refreshToken();
      if (result) {
        this.categories.createCategory(this.categoryForm.value.name);
        return;
      }
      else {
        this.router.navigateByUrl('/login');
      }
    }
  }
}
