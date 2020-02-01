"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
const common_1 = require("@nestjs/common");
let AjaxTestController = class AjaxTestController {
    Index() {
        return { message: 'Hello world!' };
    }
    async get1(res) {
        setTimeout((function () { res.send(""); }), 2000);
    }
    async get2(res) {
        res.send("OK");
    }
};
__decorate([
    common_1.Get(),
    common_1.Render('ajax-test/index'),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], AjaxTestController.prototype, "Index", null);
__decorate([
    common_1.Get('get1'),
    __param(0, common_1.Res()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Object]),
    __metadata("design:returntype", Promise)
], AjaxTestController.prototype, "get1", null);
__decorate([
    common_1.Get('get2'),
    __param(0, common_1.Res()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Object]),
    __metadata("design:returntype", Promise)
], AjaxTestController.prototype, "get2", null);
AjaxTestController = __decorate([
    common_1.Controller('ajax-test')
], AjaxTestController);
exports.AjaxTestController = AjaxTestController;
//# sourceMappingURL=ajax-test.controller.js.map