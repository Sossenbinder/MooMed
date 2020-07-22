"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
exports.useMapState = () => {
    const [map] = React.useState(new Map());
    const get = React.useCallback((key) => map.get(key), [map]);
    const set = React.useCallback((key, value) => map.set(key, value), [map]);
    const has = React.useCallback((key) => map.has(key), [map]);
    return {
        get,
        set,
        has,
    };
};
exports.default = exports.useMapState;
//# sourceMappingURL=useMapState.js.map