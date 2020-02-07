export class Contract {
  from: Part;
  to: Part;
  finished: boolean;
}

export class Part {
  id: string;
  name: string;
  address: string;
  phone: string;
}

export class AddContract {
  fromId: string;
  toId: string;
}
