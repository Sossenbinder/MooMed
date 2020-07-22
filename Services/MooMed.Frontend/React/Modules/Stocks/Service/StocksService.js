"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const ModuleService_1 = require("modules/common/Service/ModuleService");
const stocksCommunication = require("modules/Stocks/Communication/StocksCommunication");
const ExchangeTradedsReducer_1 = require("modules/Stocks/Reducer/ExchangeTradedsReducer");
class StocksService extends ModuleService_1.default {
    constructor() {
        super();
    }
    start() {
        return __awaiter(this, void 0, void 0, function* () {
            const stocksResponse = yield stocksCommunication.getExchangeTradeds();
            if (stocksResponse.success) {
                this.dispatch(ExchangeTradedsReducer_1.reducer.add(stocksResponse.payload));
            }
        });
    }
}
exports.default = StocksService;
//# sourceMappingURL=StocksService.js.map