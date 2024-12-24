export interface Message {
  id: string;
  chatId: string;
  senderId: string;
  sendedByYou: boolean;
  timeStamp: Date;
  content: string;
}
