﻿using DevExpress.XtraBars.Navigation;
using System.Collections.Generic;

namespace Fihirana_database.Classes
{
    internal class dataThematique
    {
        #region Accordion
        public static void initAccordion(AccordionControl accordionCtrl)
        {
            IDictionary<string, ChantModel[]> dict = new Dictionary<string, ChantModel[]>
            {
                ["Dieu le Père"] = new ChantModel[]
            {
                new ChantModel("Son amour", "7/001-009"),
                new ChantModel("Sa majesté", "7/010-012"),
                new ChantModel("Son pouvoir créateur", "7/013-019"),
            },

                ["Dieu le Fils"] = new ChantModel[]
            {
                new ChantModel("Son amour", "7/020-026"),
                new ChantModel("Sa naissance", "7/027-042"),
                new ChantModel("Son ministère", "7/043-057"),
                new ChantModel("Ses souffrances, sa mort", "7/058-068"),
                new ChantModel("Sa résurrection, son ascension", "7/069-077"),
                new ChantModel("Son intercession", "7/078-081"),
                new ChantModel("Son retour, son règne", "7/082-109"),
            },
                ["Dieu le Saint-Esprit"] = new ChantModel[]
            {
                new ChantModel("Saint-Esprit", "7/110-121"),
            },
                ["Le culte"] = new ChantModel[]
            {
                new ChantModel("Les Psaumes", "7/122-147"),
                new ChantModel("Les louanges", "7/148-189"),
                new ChantModel("Matin et soir", "7/190-201"),
                new ChantModel("L'ouverture", "7/202-216"),
                new ChantModel("La confession", "7/217-223"),
                new ChantModel("Les offrandes", "7/224-229"),
                new ChantModel("L'écoute de la Parole", "7/230-243"),
                new ChantModel("L'envoi", "7/244-253"),
                new ChantModel("Réponse", "7/254-261"),
            },

                ["La bonne nouvelle"] = new ChantModel[]
            {
                new ChantModel("Invitation", "7/262-263"),
                new ChantModel("Le repentir", "7/264-278"),
                new ChantModel("Le pardon", "7/279-284"),
                new ChantModel("L'assurance du salut", "7/285-297"),
                new ChantModel("L'engagement", "7/298-322"),
                new ChantModel("Croissance en Christ", "7/323-331"),
            },

                ["Convictions"] = new ChantModel[]
            {
                new ChantModel("Les Saintes Ecritures", "7/332-335"),
                new ChantModel("Le sabbat", "7/336-343"),
                new ChantModel("L'Eglise", "7/344-348"),
                new ChantModel("Jugement", "7/349-350"),
                new ChantModel("Victoire sur la mort", "7/351-359"),
            },

                ["Lieux et temps de la foi"] = new ChantModel[]
            {
                new ChantModel("Communion fraternelle", "7/360-372"),
                new ChantModel("La famille", "7/373-375"),
                new ChantModel("La mission de l'Eglise", "7/376-387"),
                new ChantModel("Le baptême", "7/388-393"),
                new ChantModel("La cène", "7/394-405"),
                new ChantModel("Evènements divers", "7/406-420"),
            },
                ["Expression de la foi"] = new ChantModel[]
           {
                new ChantModel("L'amour", "7/421-424"),
                new ChantModel("La joie", "7/425-428"),
                new ChantModel("La gratitude", "7/429-436"),
                new ChantModel("La paix et la confiance", "7/437-475"),
                new ChantModel("La consolation", "7/476-484"),
                new ChantModel("L'humilité et l'obéissance", "7/485-494"),
                new ChantModel("La prière", "7/495-502"),
                new ChantModel("La persévérance et la victoire", "7/503-520"),
           }
            };

            AccordionControlElement grRoot = new AccordionControlElement(ElementStyle.Group) { Text = "Donnez-lui gloire" };
            foreach (var item in dict)
            {
                _ = item.Key;
                AccordionControlElement gr = new AccordionControlElement(ElementStyle.Group) { Text = item.Key };

                foreach (ChantModel cht in item.Value)
                {
                    AccordionControlElement iElement = new AccordionControlElement(ElementStyle.Item)
                    {
                        Text = cht.Description,
                        Tag = cht.Range
                    };
                    gr.Elements.Add(iElement);
                }

                grRoot.Elements.Add(gr);
            }
            accordionCtrl.Elements.Add(grRoot);
        }
        #endregion
    }
}