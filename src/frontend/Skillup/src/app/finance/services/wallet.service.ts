import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { Wallet } from "../models/wallet.model";
import { environment } from "../../../environments/environment";
import { tap } from "rxjs";

@Injectable({ providedIn: 'root' })
export class WalletService {
    
  private httpClient = inject(HttpClient);
  currentWallet = signal<Wallet | null>(null);

  public getWallet(userId: string) {
    this.httpClient
      .get<Wallet>(environment.apiUrl + '/Finances/Wallets/' + userId, {})
      .pipe(
        tap((res) => {
          this.currentWallet.set(res);
        })
      ).subscribe();
  }
}