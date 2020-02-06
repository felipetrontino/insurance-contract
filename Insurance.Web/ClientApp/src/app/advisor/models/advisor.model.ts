export class Advisor {
  id: string;
  name: string;
  lastName: string;
  address: string;
  phone: string;
  healthStatus: string;
}

export enum HealthStatus {
  None = 0,
  Green = 70,
  Red = 30  
}
