import { Message } from './message.model';

export interface Chat {
  id: string;
  courseId: string;
  userId: string;
  authorId: string;

  lastMessage: Message | null;
}
