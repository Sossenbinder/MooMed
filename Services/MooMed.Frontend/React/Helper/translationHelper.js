"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.formatTranslation = (translation, ...formatPastes) => {
    let toFormat = translation;
    for (let i = 0; i < formatPastes.length; ++i) {
        toFormat = toFormat.replace(`{${i}}`, formatPastes[i]);
    }
    return toFormat;
};
//# sourceMappingURL=translationHelper.js.map