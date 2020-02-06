export class ContractViewModel {
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

export class ContractInputModel {
  
  fromId: string;
  toId: Part; 
}