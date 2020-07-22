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
const Flex_1 = require("common/Components/Flex");
const Icon_1 = require("common/Components/Icon");
const useServices_1 = require("hooks/useServices");
require("./ActionCell.less");
exports.ActionCell = ({ rowData }) => {
    const { PortfolioService } = useServices_1.default();
    return (React.createElement(Flex_1.default, { className: "ShowOnHover ActionCell" },
        React.createElement(Icon_1.default, { className: "Plus", iconName: "plusSign", size: 20, onClick: () => __awaiter(void 0, void 0, void 0, function* () { return yield PortfolioService.addToPortfolio(rowData.isin, 1); }) })));
};
exports.default = exports.ActionCell;
//# sourceMappingURL=ActionCell.js.map