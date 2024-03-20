import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { enviroment } from './enviroment/enviroment';
import { Injectable, inject } from '@angular/core';
import { AuthService } from './auth-service';

@Injectable({
    providedIn: 'root',
})

export class SignalRService {
    hubConnection: signalR.HubConnection;
    api = enviroment.Hub;
    auth = inject(AuthService);

    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(this.api, { headers: { Authorization: `Bearer ${this.auth.getJwtToken()}` } })
            .build();
    }

    startConnection(): Observable<void> {
        if (this.hubConnection.state === signalR.HubConnectionState.Connected) {
            return new Observable<void>((observer) => {
                observer.add()
                observer.next();
                observer.complete();
            });
        }

        return new Observable<void>((observer) => {
            this.hubConnection
                .start()
                .then(() => {
                    observer.next();
                    observer.complete();
                })
                .catch((error) => {
                    observer.error(error);
                });
        });
    }

    public sendNoteAsync(noteCommand: any): Promise<void> {
        return this.hubConnection.invoke('SendNoteAsync', noteCommand);
      }
    
      public deleteNoteAsync(noteCommand: any): Promise<void> {
        return this.hubConnection.invoke('DeleteNoteAsync', noteCommand);
      }
    
      public sendUserProgressAsync(progressCommand: any): Promise<void> {
        return this.hubConnection.invoke('SendUserProgressAsync', progressCommand);
      }
    
      public onReceiveNote(listener: (note: any) => void): void {
        this.hubConnection.on('ReceiveNote', listener);
      }
    
      public onDeleteNote(listener: (noteId: string) => void): void {
        this.hubConnection.on('DeleteNote', listener);
      }
    
      public onReceiveUserProgress(listener: (progress: any) => void): void {
        this.hubConnection.on('ReceiveUserProgress', listener);
      }

}