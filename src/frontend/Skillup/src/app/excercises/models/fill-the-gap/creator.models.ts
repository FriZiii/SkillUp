export default class Part {
  constructor(type: PartType) {
    this.type = type;
  }

  value: string = '';
  type: PartType;
}

export enum PartType {
  Sentence = 1,
  Word = 2,
}

export interface FakeWord {
  value: string;
}
