import { Test, TestingModule } from '@nestjs/testing';
import { AjaxTestController } from './ajax-test.controller';

describe('AjaxTest Controller', () => {
  let controller: AjaxTestController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [AjaxTestController],
    }).compile();

    controller = module.get<AjaxTestController>(AjaxTestController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
