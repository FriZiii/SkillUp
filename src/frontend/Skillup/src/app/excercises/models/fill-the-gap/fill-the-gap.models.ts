export interface Word {
  index: number;
  value: string;
}

export interface Sentence {
  id: string;
  value: string;
  words: Word[];
}

export class Sentence {
  constructor() {
    (this.value = ''), (this.words = []);
  }
  value: string;
  words: Word[];
}
