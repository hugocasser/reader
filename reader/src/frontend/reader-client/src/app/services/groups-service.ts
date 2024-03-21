import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable, inject } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { enviroment } from "./enviroment/enviroment";
import { AuthService } from "./auth-service";
import { GroupItem } from "../models/groups";


@Injectable({
    providedIn: 'root'
})
export class GroupService {
    private apiUrl = enviroment.Group + 'api/groups';
    private auth = inject(AuthService);

    constructor(private http: HttpClient) { }
    createGroup(groupData: any): Observable<any> {
        return this.http.post<any>(this.apiUrl, groupData, {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}}).pipe(
            catchError(this.handleError)
        );
    }

    getGroupByUserId(pageSize: number, page: number): Observable<any[]> {
        let userId = localStorage.getItem('userId');
        return this.http.get<any[]>(`${this.apiUrl}/user/${userId}?page=${page}&pageSize=${pageSize}` ,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}}).pipe(
            catchError(this.handleError)
        );
    }

    updateGroup(groupId: string, groupData: any): Observable<any> {
        return this.http.put<any>(`${this.apiUrl}/${groupId}`, groupData,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}}).pipe(
            catchError(this.handleError)
        );
    }

    deleteGroup(groupId: string): Observable<any> {
        return this.http.delete<any>(`${this.apiUrl}/${groupId}`,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}}).pipe(
            catchError(this.handleError)
        );
    }

    getGroupById(groupId: string): Observable<any> {
        return this.http.get<any>(`${this.apiUrl}/${groupId}`,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}}).pipe(
            catchError(this.handleError)
        );
    }

    getAllGroups(pageSize: number, page: number): any[] {
        let resp: any[] = [];
        this.http.get<any[]>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}` ,  {headers: {Authorization: `Bearer ${this.auth.getJwtToken()}`}}).pipe(
            catchError(this.handleError)
        ).subscribe({
            next: (data) => {
                resp = data;
            },
            complete: () => {
            }
        });
        return resp;
    }

    private handleError(error: HttpErrorResponse) {
        let errorMessage = 'An error occurred';
        if (error.error instanceof ErrorEvent) {
            errorMessage = `Error: ${error.error.message}`;
        } else {
            errorMessage =error.status.toString();
        }
        return throwError(() => new Error(errorMessage));
    }
}
