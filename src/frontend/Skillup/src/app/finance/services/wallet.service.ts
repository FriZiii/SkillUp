import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { WalletWithBalanceHistory } from "../models/wallet.model";
import { environment } from "../../../environments/environment";
import { tap } from "rxjs";

@Injectable({ providedIn: 'root' })
export class WalletService {
    
  private httpClient = inject(HttpClient);
  currentWallet = signal<WalletWithBalanceHistory | null>(null);

  public getWallet(userId: string) {
    this.httpClient
      .get<WalletWithBalanceHistory>(environment.apiUrl + '/Finances/Wallets/' + userId, {})
      .pipe(
        tap((res) => {
          this.currentWallet.set(res);
        })
      ).subscribe();
  }

  setWallet(balance: number){
    this.currentWallet.set({
      id: this.currentWallet()!.id,
      balance: balance,
      userId: this.currentWallet()!.userId,
      balanceHistory: this.currentWallet()!.balanceHistory
    })
  }

  public addBalance(balance: number){
    return this.httpClient
      .put<WalletWithBalanceHistory>(environment.apiUrl + '/Finances/Wallets/' + this.currentWallet()?.id + '?balance=' + balance, {})
      .pipe(
        tap((res) => {
          this.currentWallet.set(res);
          //this.getWallet(this.userService.currentUser()!.id);
        })
      )
  }
}