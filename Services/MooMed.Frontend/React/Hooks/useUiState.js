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
const React = require("react");
exports.useUiState = (initialValue) => {
    const [uiSettings, setUiSettings] = React.useState(initialValue);
    const updateState = React.useCallback((updatedVal) => __awaiter(void 0, void 0, void 0, function* () {
        setUiSettings(updatedVal);
    }), [setUiSettings]);
    return [uiSettings, updateState];
};
exports.default = exports.useUiState;
//# sourceMappingURL=useUiState.js.map