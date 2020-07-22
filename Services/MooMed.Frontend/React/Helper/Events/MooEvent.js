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
const Array = require("helper/arrayUtils");
class MooEvent {
    constructor() {
        this.listenerCounter = 1;
        this.registeredListeners = [];
    }
    Register(func) {
        const listenerId = this.listenerCounter;
        this.listenerCounter++;
        this.registeredListeners.push({
            listenerId,
            listener: func,
        });
        return listenerId;
    }
    Unregister(listenerId) {
        const respectiveListenerInfoIndex = this.registeredListeners.findIndex(x => x.listenerId === listenerId);
        Array.removeAt(this.registeredListeners, respectiveListenerInfoIndex);
    }
    Raise(args) {
        return __awaiter(this, void 0, void 0, function* () {
            yield Promise.all(this.registeredListeners.map(registeredListener => {
                registeredListener.listener(args);
            }));
        });
    }
}
exports.default = MooEvent;
//# sourceMappingURL=MooEvent.js.map