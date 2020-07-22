"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const store_1 = require("data/store");
class ModuleService {
    constructor() {
        this.Store = store_1.store;
    }
    getStore() {
        return this.Store.getState();
    }
    dispatch(dispatchAction) {
        this.Store.dispatch(dispatchAction);
    }
}
exports.default = ModuleService;
//# sourceMappingURL=ModuleService.js.map