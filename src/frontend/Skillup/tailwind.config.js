module.exports = {
  darkMode: "selector",
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      width: {
        '100': '28rem',   // 448px
        '120': '32rem',   // 512px
        '128': '36rem',   // 576px
        '192': '48rem',   // 768px
      },
    },
  },
  plugins: [
     require("tailwindcss-primeui"),    
     require('@tailwindcss/typography')
    ],
};
