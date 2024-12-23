export interface Wallet{
    id: string;
    balance: number;
    userId: string;
}

export interface WalletWithBalanceHistory{
    id: string;
    balance: number;
    userId: string;
    balanceHistory: BalanceHistory[];
}

export interface BalanceHistory{
    id: string;
    amount: number;
    date: Date;
    title: string;
    type: string;
}