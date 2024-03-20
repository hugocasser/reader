import { Component, inject } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth-service';
import { ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, CommonModule, ReactiveFormsModule, NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;
  isVisible: boolean = true;
  authService = inject(AuthService);

  constructor(private router: Router, private formBuilder: FormBuilder){
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(32)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', Validators.required]
    });
  }

  get formControls() {
    return this.registerForm.controls;
  }

  Register(){
  }

  RedirectToLogin(){
    this.router.navigate(['/login']);
    this.isVisible = true;
  }
}
