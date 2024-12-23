import { Component, input } from '@angular/core';
import { Message } from '../../../../models/message.model';

@Component({
  selector: 'app-message-item',
  standalone: true,
  imports: [],
  templateUrl: './message-item.component.html',
  styleUrl: './message-item.component.css',
})
export class MessageItemComponent {
  message = input.required<Message>();
}
