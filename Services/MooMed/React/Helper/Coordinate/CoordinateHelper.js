"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function IsInRect(rect, x, y) {
    return y < rect.bottom && y > rect.top && x < rect.right && x > rect.left;
}
exports.IsInRect = IsInRect;
//# sourceMappingURL=CoordinateHelper.js.map