/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Components/**/*.{razor,html,cshtml}',
    './Pages/**/*.{razor,html,cshtml}',
    './wwwroot/**/*.html',
    './**/*.razor'
  ],
  theme: {
    extend: {
      colors: {
        terracotta: {
          50: '#FDF6F2',
          100: '#FBECE5',
          200: '#F6D7C9',
          300: '#EEB7A2',
          400: '#DF8E6F',
          500: '#CB6D48',
          600: '#C0613D',
          700: '#B55B3A',
          800: '#96472A',
          900: '#7B3B24'
        },
        espresso: {
          700: '#4D403A',
          800: '#3E3430',
          900: '#2D2522',
          950: '#221C1A'
        },
        sand: {
          50: '#FAF8F5',
          100: '#FAF4EE',
          200: '#F3E9DD',
          300: '#EAE0D2',
          400: '#D5C6B3'
        },
        brand: {
          primary: '#3E3430',
          accent: '#B55B3A',
          terracotta: '#B55B3A',
          bg: '#FAF8F5',
          callout: '#FAF4EE'
        }
      },
      fontFamily: {
        serif: ['Newsreader', 'Playfair Display', 'Georgia', 'serif'],
        sans: ['"Plus Jakarta Sans"', 'Inter', 'system-ui', 'sans-serif']
      }
    }
  },
  plugins: []
};
