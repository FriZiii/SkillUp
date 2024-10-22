import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-account-activation',
  standalone: true,
  imports: [],
  templateUrl: './account-activation.component.html',
  styleUrl: './account-activation.component.css',
})
export class AccountActivationComponent implements OnInit {
  authService = inject(AuthService);
  route = inject(ActivatedRoute);

  userId!: string;
  activationToken!: string;

  ngOnInit(): void {
    this.userId = this.route.snapshot.queryParamMap.get('userId')!;
    this.activationToken =
      this.route.snapshot.queryParamMap.get('activationToken')!;

    this.activateAccount();
  }

  activateAccount() {
    this.authService
      .activateAccount(this.userId, this.activationToken)
      .subscribe({
        next: (res: any) => {
          console.log(res);
        },
      });
  }
}
