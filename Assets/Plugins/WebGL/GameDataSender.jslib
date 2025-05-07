

mergeInto(LibraryManager.library, {


  sendGameDataToWeb: function(jsonDataPtr) {
    console.log("JSlib function 'sendGameDataToWeb' called from Unity."); // Лог для дебагу

    var jsonData = UTF8ToString(jsonDataPtr);
    console.log("Data received in JSlib:", jsonData); // Лог для дебагу

    // Викликаємо глобальну функцію, визначену у вашому Vue додатку (або будь-якому іншому JS на сторінці)
    if (typeof window.sendGameDataToWeb === 'function') {
      console.log("Calling window.sendGameDataToWeb..."); // Лог для дебагу
      window.sendGameDataToWeb(jsonData);
    } else {
      console.error("Error: window.sendGameDataToWeb is not defined! Make sure your Vue component is mounted and defines this function.");
      
    }
  },

});