export class ExchangeRate {
    currencyFromId: string;
    currencyToId: string;
    exchangeRate: number;
    amount: number;
    amountExchange: number;

    constructor(){
        this.currencyFromId = '';
        this.currencyToId = '';
        this.exchangeRate = 0;
        this.amount = 0;
        this.amountExchange = 0;
    }
}