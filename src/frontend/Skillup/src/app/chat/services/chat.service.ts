import { inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { Chat } from '../models/chat.model';
import { HttpClient } from '@angular/common/http';
import { Message } from '../models/message.model';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private hubConnection!: signalR.HubConnection;
  private httpClient = inject(HttpClient);

  //SignalR
  startConnection(token: string, chatId: string) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/chatHub?chatId=${chatId}`, {
        accessTokenFactory: () => token,
      })
      .build();

    this.hubConnection
      .start()
      .catch((err) => console.error('Error while starting connection: ' + err));
  }

  disconnect() {
    if (this.hubConnection) {
      this.hubConnection
        .stop()
        .then(() => console.log('Connection stopped'))
        .catch((err) =>
          console.error('Error while stopping connection: ', err)
        );
    }
  }

  sendMessage(chatId: string, message: string) {
    this.hubConnection
      .invoke('SendMessage', chatId, message)
      .catch((err) => console.error(err));
  }

  onReceiveMessage(callback: (message: any) => void) {
    this.hubConnection.on('ReceiveMessage', (message) => {
      callback(message);
    });
  }

  //HTTP Client
  public fetchChats(userId: string) {
    return this.httpClient.get<Chat[]>(
      environment.apiUrl + '/Chats/Users/' + userId
    );
  }

  public fetchChatHistory(chatId: string) {
    return this.httpClient.get<Message[]>(
      environment.apiUrl + '/Chats/' + chatId + '/Messages'
    );
  }
}
