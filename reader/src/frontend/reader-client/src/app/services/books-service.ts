import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AuthService } from './auth-service';
import { enviroment } from './enviroment/enviroment';
import { throwError } from 'rxjs';
import { Book } from '../models/book';

@Injectable({
    providedIn: 'root'
  })
export class BooksService {
    private http = inject(HttpClient);
    private auth = inject(AuthService);
    readonly route = enviroment.Book;
    errors ='';

    GetAllBooks(page: number, pageSize: number) : Book[]{
        let resp: Book[] = [];
        this.http.get<Book[]>(`${this.route}books?page=${page}&pageSize=${pageSize}`, {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
        .subscribe({
            next: (response) => {
                resp = response;
            },
            error: (error) =>{
                this.handleError(error);
            },
            complete:() =>{}
        });

        return resp;
    }

    GetBooksByAuthor(authorId: string, page: number, pageSize: number): Book[] |any{
        this.http.get<Book[]>(`${this.route}/api/authors/${authorId}books?page=${page}&pageSize=${pageSize}`, {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
        .subscribe({
            next: (response) => {
                return response;
            },
            error: (error) =>{
                return this.handleError(error);
            },
            complete:() =>{}
        })
    }

    GetBooksByCategory(categoryId: string, page: number, pageSize: number): Book[] | any{
        this.http.get<Book[]>(`${this.route}/api/books/categories/${categoryId}/books?page=${page}&pageSize=${pageSize}`, {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
        .subscribe({
            next: (response) => {
                return response;
            },
            error: (error) =>{
                return this.handleError(error);
            },
            complete:() =>{}
        })
    }
    
    GetBookById(id: string): Book | any{

        this.http.get<Book>(`${this.route}books/${id}`, {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
        .subscribe({
            next: (response) => {
                return response;
            },
            error: (error) =>{
                return this.handleError(error);
            },
            complete:() =>{}
        })
    }

    CreateBook(name: string, text: string, description: string, authorId: string, categoryId: string): any{
        this.http.post(`${this.route}books`, {description, name, text, authorId, categoryId}, {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
        .subscribe({
            next: (response) => {
                return response;
            },
            error: (error) =>{
                return this.handleError(error);
            },
            complete:() =>{}
        })
        return false;
    }

    DeleteBook(id: string): boolean{
        this.http.delete(`${this.route}books/${id}`, {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
        .subscribe({
            next: (response) => {
                return response;
            },
            error: (error) =>{
                return this.handleError(error);
            },
            complete:() =>{}
        })
        return false;
    }

    private handleError(error: any): any{

    if (error instanceof HttpErrorResponse) {
      this.errors = error.status.toString();
      
      return throwError(() => new Error(error.status.toString()));
    }
    
    this.errors = 'error'
    
    return throwError(error);
    }

    clearErrors(){
        this.errors = '';
    }
}