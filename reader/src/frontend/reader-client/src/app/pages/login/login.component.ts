import { CommonModule, NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [NgIf, RouterLink, CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent implements OnInit {
  loginForm: FormGroup; 
  isVisible: boolean = true; 
  authService = inject(AuthService); // Define FormGroup for the form
  isOk = true;

  constructor(private formBuilder: FormBuilder, private router: Router) { 
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(32)]],
    });
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]], // Add validators for email
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(32)]] // Add validators for password
    });
  }

  // Convenience getter for easy access to form controls
  get f() { return this.loginForm.controls; }

  Login() {
    if (this.loginForm.invalid) {
      return; // Do not proceed if form is invalid
    }

    // Implement login logic
  }

  RedirectToRegister(){
    this.router.navigateByUrl('/register');
  }
}


