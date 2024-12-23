import { Component, input, output } from '@angular/core';
import { Chat } from '../../../models/chat.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-chat-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './chat-item.component.html',
  styleUrl: './chat-item.component.css',
})
export class ChatItemComponent {
  chat = input.required<Chat>();
  onSelected = output<Chat>();

  isSelected = input.required<boolean>();

  onSelect() {
    this.onSelected.emit(this.chat());
  }
}
