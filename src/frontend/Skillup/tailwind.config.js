module.exports = {
  darkMode: "selector",
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {},
  },
  plugins: [
     require("tailwindcss-primeui"),    
     require('@tailwindcss/typography')
    ],
};
