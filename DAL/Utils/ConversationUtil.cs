using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Utils
{
    public static class ConversationUtil
    {
        private static readonly HContext _context = new HContext();

        public static void AddConversation(mp_conversation conversation)
        {
            try
            {
                conversation.created_at = DateTime.Now;
                _context.mp_conversation.Add(conversation);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {

            }
           
        }

        public static IQueryable<mp_conversation> GetConversations(string first_person, string second_person)
        {
            return _context.mp_conversation.Where(e => (e.from == first_person && e.to == second_person) || (e.to == first_person && e.from == second_person));
        }

        public static List<ConversationItem> GetUserChannels(Guid profile_id,string profile_type)
        {
            var conversation_items = new List<ConversationItem>();
            //get all the appointments of the person
            if (profile_type == "client")
            {
                var profile = _context.mp_profile.FirstOrDefault(e => e.id == profile_id);
                var ids = _context.mp_appointment.Where(e => e.client_id == profile_id).Select(e=>e.clinician_id).Distinct();
                //get all the clinicians
                var clinicians = _context.mp_clinician.Where(e => ids.Contains(e.id)).ToList();

                foreach(var clinician in clinicians)
                {
                    var conversation = _context.mp_conversation.Where(e => (e.from == clinician.user_id && e.to == profile.user_id) || (e.from==profile.user_id && e.to==clinician.user_id)).OrderBy(e=>e.created_at).FirstOrDefault();

                    var conversation_item = new ConversationItem
                    {
                        person_id = clinician.id,
                        name = string.Format("{0} {1}", clinician.last_name, clinician.first_name),
                        user_id = clinician.user_id
                    };

                    if (conversation != null)
                    {
                        conversation_item.message = conversation.message;
                    }

                    conversation_items.Add(conversation_item);
                }
            }
            else if (profile_type == "clinician")
            {
                var profile = _context.mp_clinician.FirstOrDefault(e => e.id == profile_id);
                var ids = _context.mp_appointment.Where(e => e.clinician_id == profile_id).Select(e => e.client_id).Distinct();
                //get all the clinicians
                var clients = _context.mp_clinician.Where(e => ids.Contains(e.id));

                foreach (var client in clients)
                {
                    var conversation = _context.mp_conversation.LastOrDefault(e => (e.from == client.user_id && e.to == profile.user_id) || (e.from == profile.user_id && e.to == client.user_id));

                    var conversation_item = new ConversationItem
                    {
                        person_id = client.id,
                        name = string.Format("{0} {1}", client.last_name, client.first_name),
                        user_id = client.user_id
                    };

                    if (conversation != null)
                    {
                        conversation_item.message = conversation.message;
                    }

                    conversation_items.Add(conversation_item);
                }
            }

            return conversation_items;
            


        }
    }
}
