import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { enviroment } from './enviroment/enviroment';
import { AuthService } from './auth-service';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  private baseUrl = enviroment.Group + 'api/notes';
  private http = inject(HttpClient);
  private auth = inject(AuthService);

  getAllUserNotes(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/users`,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
      .pipe(
        catchError(this.handleError)
      );
  }

  getAllUserNotesInGroup(groupId: string, userId: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/groups/${groupId}/users/${userId}`,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
      .pipe(
        catchError(this.handleError)
      );
  }

  getNoteById(noteId: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${noteId}`,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
      .pipe(
        catchError(this.handleError)
      );
  }

  createNote(note: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/`, note,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteNote( noteId: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/notes/${noteId}`,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}})
      .pipe(
        catchError(this.handleError)
      );
  }

  getAllGroupNotes(groupId: string, pageSettingsRequestDto: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${groupId}`, { params: pageSettingsRequestDto, headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`} }, )
      .pipe(
        catchError(this.handleError)
      );
  }

  getNotesByGroupIdAndBookId(groupId: string, bookId: string, pageSettingsRequestDto: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/groups/${groupId}/books/${bookId}`, { params: pageSettingsRequestDto, headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`} }, )
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = 'error';
    } else {
      errorMessage = error.status.toString();
    }

    return throwError(() => new Error(errorMessage));
  }
}