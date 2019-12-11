export interface OperatorModel {
  action: string,
  path: string,
  value: string | string[],
  guid?: string
}

export class OperatorModelStatus implements OperatorModel {
  action: string;
  path: string;
  value: string | string[];
  guid?: string;
  status?: 'pending' | 'inprogess' | 'done' | 'failed';
  constructor() {
    this.status = 'pending';
  }
}
