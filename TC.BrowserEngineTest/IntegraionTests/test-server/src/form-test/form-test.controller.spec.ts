import { Test, TestingModule } from '@nestjs/testing';
import { FormTestController } from './form-test.controller';

describe('FormTest Controller', () => {
  let controller: FormTestController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [FormTestController],
    }).compile();

    controller = module.get<FormTestController>(FormTestController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
