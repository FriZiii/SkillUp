import { Component, input } from '@angular/core';
import { Message } from '../../../../models/message.model';
import { User } from '../../../../../user/models/user.model';
import { TimeAgoPipe } from '../../../../../utils/pipes/timeAgo.pipe';

@Component({
  selector: 'app-message-item',
  standalone: true,
  imports: [TimeAgoPipe],
  templateUrl: './message-item.component.html',
  styleUrl: './message-item.component.css',
})
export class MessageItemComponent {
  message = input.required<Message>();
  currentUser = input.required<User | null>();
  talker = input.required<User | null>()

  ngOnInit(){
    console.log(this.currentUser())
    console.log(this.talker())
  }
}
