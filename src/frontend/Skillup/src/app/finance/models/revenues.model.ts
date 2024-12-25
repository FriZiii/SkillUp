export interface ItemEarnings{
    itemId: string;
    itemsCount: number;
    total: number;
}

export interface YearEarnings{
    year: number;
    months: string[];
    monthlyEarnings: MonthlyEarnings[];
}

export interface MonthlyEarnings{
    itemId: string;
    data: number[];
}

export interface Revenue{
    totalRevenue: number;
    beginDate: Date;
    lastWeekRevenue: number;
    changePercentage: number;
    itemsCovered: number;
}

export const greenShades = [
    '#004d00', '#4AA764', '#155226', '#256738', '#2F8446',
    '#117411', '#409040', '#5a995a', '#81aa81', '#297b29',
    '#005200', '#308030', '#639c63', '#6da06d', '#77a577',
    '#005900', '#0a720a', '#207820', '#499349', '#529652'
];