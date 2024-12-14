export interface Item{
    id: string;
    authorid: string;
    type: ItemType;
    price:  number;
}

export enum ItemType {
    Course = 0,
  }
  