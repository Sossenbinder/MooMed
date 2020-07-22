"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ensureArray = (data) => {
    if (data instanceof Array) {
        return data;
    }
    else {
        return [data];
    }
};
exports.removeAt = (arr, index) => {
    arr.splice(index, 1);
};
exports.split = (arr, splitSize) => {
    const copiedArray = [...arr];
    const result = [];
    while (copiedArray.length > 0) {
        result.push(copiedArray.splice(0, splitSize));
    }
    return result;
};
//# sourceMappingURL=arrayUtils.js.map