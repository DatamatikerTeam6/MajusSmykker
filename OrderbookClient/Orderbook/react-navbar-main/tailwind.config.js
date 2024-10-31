module.exports = {
  mode: "jit",
  darkMode: "class",
  content: [
    "./src/**/*.{js,ts,jsx,tsx,html,mdx}",
    "./src/**/.{js,ts,jsx,tsx,html,mdx}",
  ],
  theme: {
    screens: {
      lg: { max: "1440px" },
      md: { max: "1050px" },
      sm: { max: "550px" },
    },
    extend: {
      colors: {
        black: { 900: "var(--black_900)" },
        cyan: { 100: "var(--cyan_100)" },
        gray: { 900: "var(--gray_900)" },
        white: { a700: "var(--white_a700)" },
      },
      boxShadow: {},
      fontFamily: {
        librefranklin: "Libre Franklin",
      },
    },
  },
  plugins: [require("@tailwindcss/forms")],
};
