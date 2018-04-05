import fontawesome from '@fortawesome/fontawesome'
// Official documentation available at: https://github.com/FortAwesome/vue-fontawesome
import FontAwesomeIcon from '@fortawesome/vue-fontawesome'
import { faEnvelope, faHome, faGraduationCap, faList, faSpinner, faShoppingCart, faThList, faCreditCard, faSignOutAlt, faSignInAlt, faCog, faSearch, faUserCircle, faPlus } from '@fortawesome/fontawesome-free-solid'
import { faMicrosoft, faVuejs, faFontAwesome } from '@fortawesome/fontawesome-free-brands'

// If not present, it won't be visible within the application. Considering that you
// don't want all the icons for no reason. This is a good way to avoid importing too many
// unnecessary things.
fontawesome.library.add(
  faEnvelope,
  faGraduationCap,
  faHome, 
  faList,
  faSpinner, 
  faShoppingCart,
  faThList,
  faCreditCard,
  faSignInAlt,
  faSignOutAlt,
  faCog,
  faSearch,
  faPlus,
  faUserCircle,
  faFontAwesome,
  faMicrosoft, 
  faVuejs,
)

export {
  FontAwesomeIcon
}
