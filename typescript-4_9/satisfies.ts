type RGB = [red: number, green: number, blue: number];

const palette = {
  red: [255, 0, 0],
  green: "#00ff00",
  blue: [0, 0, 0]
  //    ~~~~~~ error!
} satisfies Record<string, string | RGB >;

// Information about each property is still maintained.
const redComponent = palette.red[0]
console.log(redComponent) // 255, palette.redのredが取れる
const greenNormalized = palette.green.toUpperCase();