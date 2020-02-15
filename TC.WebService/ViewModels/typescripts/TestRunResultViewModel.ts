 
export interface TestRunResultViewModel { 
  seleniumCommand: SeleniumCommand;
  commandTestGuid: string;
  runTime: number;
  screenshotUrl: string;
  isSuccesful: boolean;
  createdBy: string;
  dateAdded: Date;
}

